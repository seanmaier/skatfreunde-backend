import pandas as pd
from skat_connection import login, logout, get_players, send_player, update_player, post_match_session, get_me
import json
import datetime

# 1. Check for existing players in the backend
# 2. Filter for non existing players
# 3. Send new players to the backend
# 4. Fetch all players from the backend
# 5. Prepare the payload with player stats and the player IDs
# 6. Send the payload to the backend


# TODO IMPORTANT: COOKIE WILL NOT BE SEND IF COOKIE IS SET TO SECURE

pathToExcel = "skatfreunde-data.xlsx"
df = pd.read_excel(pathToExcel)
print(df)

# ----------------Configuration------------------

# If a new database was created, set this to True
NEW_DATABASE = False

user_id = ""
YEAR_OF_DATA = 2023


# -------------------Helper Functions-------------------

def extract_match_session(row, players, created_by_user_id):
    # Get the calendar week as integer
    calendar_week = int(row["MatchDay"])

    # Calculate the Wednesday of the given calendar week
    # ISO weeks: Monday is 1, Sunday is 7
    wednesday = datetime.datetime.strptime(f'{YEAR_OF_DATA}-W{calendar_week}-3', "%G-W%V-%u")
    calendar_week = wednesday.strftime("%Y-%m-%dT%H:%M:%S.%f")[:-3] + "Z"
    match_rounds = []
    col_idx = 1  # Start after MatchDay

    nan_count = 0
    while col_idx < len(row):
        name = row.iloc[col_idx]
        if pd.isna(name):
            nan_count += 1
            if nan_count >= 5:
                break
            col_idx += 9  # Skip to next player block
            continue
        else:
            nan_count = 0  # Reset if a valid player is found

        # Check if Match 1 is NaN, skip this group if so
        if pd.isna(row.iloc[col_idx + 1]):
            col_idx += 9
            continue

        # Map name to player ID
        player_id = None
        for p in players:
            if p["name"] == name:
                player_id = p["id"]
                break

        # Extract stats for Match 1
        match1_points = int(row.iloc[col_idx + 1]) if not pd.isna(row.iloc[col_idx + 1]) else 0
        match1_won = int(row.iloc[col_idx + 2]) if not pd.isna(row.iloc[col_idx + 2]) else 0
        match1_lost = int(row.iloc[col_idx + 3]) if not pd.isna(row.iloc[col_idx + 3]) else 0
        match1_table = str(int(row.iloc[col_idx + 4])) if not pd.isna(row.iloc[col_idx + 4]) else ""

        # Extract stats for Match 2
        match2_points = int(row.iloc[col_idx + 5]) if not pd.isna(row.iloc[col_idx + 5]) else 0
        match2_won = int(row.iloc[col_idx + 6]) if not pd.isna(row.iloc[col_idx + 6]) else 0
        match2_lost = int(row.iloc[col_idx + 7]) if not pd.isna(row.iloc[col_idx + 7]) else 0
        match2_table = str(int(row.iloc[col_idx + 8])) if not pd.isna(row.iloc[col_idx + 8]) else ""

        # Add to match_rounds for Match 1
        found = False
        for rnd in match_rounds:
            if rnd["RoundNumber"] == "1" and rnd["Table"] == match1_table:
                rnd["PlayerRoundStats"].append({
                    "PlayerId": player_id,
                    "Points": match1_points,
                    "Won": match1_won,
                    "Lost": match1_lost
                })
                found = True
                break
        if not found:
            match_rounds.append({
                "RoundNumber": "1",
                "Table": match1_table,
                "PlayerRoundStats": [{
                    "PlayerId": player_id,
                    "Points": match1_points,
                    "Won": match1_won,
                    "Lost": match1_lost
                }]
            })

        # Add to match_rounds for Match 2
        found = False
        for rnd in match_rounds:
            if rnd["RoundNumber"] == "2" and rnd["Table"] == match2_table:
                rnd["PlayerRoundStats"].append({
                    "PlayerId": player_id,
                    "Points": match2_points,
                    "Won": match2_won,
                    "Lost": match2_lost
                })
                found = True
                break
        if not found:
            match_rounds.append({
                "RoundNumber": "2",
                "Table": match2_table,
                "PlayerRoundStats": [{
                    "PlayerId": player_id,
                    "Points": match2_points,
                    "Won": match2_won,
                    "Lost": match2_lost
                }]
            })

        col_idx += 9  # Move to next player block

    return {
        "CreatedById": created_by_user_id,
        "PlayedAt": calendar_week,
        "MatchRounds": match_rounds
    }


def comapre_players(existing_players, new_players):
    # Compare existing players with new players
    existing_names = {player["name"] for player in existing_players}

    non_existing_players = [player for player in new_players if player not in existing_names]

    return non_existing_players


def extract_players():
    # Extract all columns that start with "Name"
    name_columns = [col for col in df.columns if str(col).startswith("Name")]
    # Get the first row's values for those columns, drop NaN, and get unique names
    # Collect all player names from all rows and all "Name" columns
    names = []
    for _, row in df.iterrows():
        for col in name_columns:
            name = row[col]
            if pd.notna(name):
                names.append(str(name))
    # Remove duplicates while preserving order
    seen = set()
    distinct_names = []
    for name in names:
        if name not in seen:
            distinct_names.append(name)
            seen.add(name)
    return distinct_names


if __name__ == "__main__":
    # player_stats = extract_datasets()
    #print("Extracted player stats:", player_stats)

    try:
        login()
        response = get_me()
        user_id = response.get("id")
        existing_players = get_players()

        if NEW_DATABASE:
            # Check for existing players in the backend
            new_players = extract_players()
            print("Extracted new players:", new_players)
            to_be_added_players = comapre_players(existing_players, new_players)
            print("New players to be added:", to_be_added_players)
            for player_name in to_be_added_players:
                send_player(user_id, player_name)
        else:
            # Add match session data for each row
            for idx, row in df.iterrows():
                dataset = extract_match_session(row, existing_players, user_id)
                print(f"Extracted dataset for row {idx}:")
                print(json.dumps(dataset, indent=4, ensure_ascii=False))
                post_match_session(dataset)

            # Example for a specific row (e.g., row 9)
            """
            dataset = extract_match_session(df.iloc[1], existing_players, user_id)
            print("Extracted dataset")
            print(json.dumps(dataset, indent=4, ensure_ascii=False))
            post_match_session(dataset)
            """



    except Exception as e:
        print(f"An error occurred: {e}")
    finally:
        logout()