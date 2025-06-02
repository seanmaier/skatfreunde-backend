import requests
from urllib.parse import urlparse
import os

# Configuration
BASE_URL = os.environ.get("SKAT_BASE_URL", "http://localhost:5000")  # MUST be HTTPS for Secure cookies
LOGIN_ENDPOINT = "/api/auth/login"
PROTECTED_ENDPOINT = "/api/Players"

CREDENTIALS = {
    "loginInput": os.environ.get("ADMIN_EMAIL", "admin"),
    "password": os.environ.get("ADMIN_PASSWORD", "Admin123!"),
    "rememberMe": False
}

session = requests.Session()

def debug_cookies(session: requests.Session, url: str):
    """Helper to debug cookie handling"""
    parsed = urlparse(url)
    print(f"\n[Debug] Cookies being sent to {parsed.netloc}:")
    cookies = session.cookies.get_dict()  # Get all cookies
    if not cookies:
        print("  No cookies will be sent!")
    for name, value in cookies.items():
        print(f"  {name}={value}")

def login():
    # Create a session

    # Configure session headers
    session.headers.update({
        "Content-Type": "application/json",
        "Accept": "application/json"
    })

    # 1. Login request
    login_url = f"{BASE_URL}{LOGIN_ENDPOINT}"
    print(f"Making login request to {login_url}")

    try:
        login_response = session.post(login_url, json=CREDENTIALS)

        # Check login response
        print(f"\nLogin Response: {login_response.status_code}")
        print("Set-Cookie headers:")
        for header in login_response.headers.get('Set-Cookie', '').split(','):
            print(f"  {header.strip()}")

        # 2. Debug cookie storage
        debug_cookies(session, BASE_URL + PROTECTED_ENDPOINT)

    except requests.exceptions.SSLError as e:
        print(f"\nSSL Error: {e}")
        print("Tip: If using self-signed cert, try session.verify = False (insecure!)")
    except Exception as e:
        print(f"\nError: {e}")


def logout():
    logout_url = f"{BASE_URL}/api/auth/logout"
    print(f"Logging out from {logout_url}")
    try:
        response = session.post(logout_url)
        print(f"Response status code: {response.status_code}")
        if response.status_code == 200:
            print("Logged out successfully!")
        else:
            print("Failed to log out:", response.text)
    except requests.exceptions.RequestException as e:
        print(f"Request failed: {e}")

def get_players():
    players_url = f"{BASE_URL}/api/players"
    print(f"Fetching players from {players_url}")
    try:
        response = session.get(players_url)
        print(f"Response status code: {response.status_code}")
        if response.status_code == 200:
            print("Players fetched successfully!")
            #print("Response data:", response.json())
            return response.json()
        else:
            print("Failed to fetch players:", response.text)
            return None
    except requests.exceptions.RequestException as e:
        print(f"Request failed: {e}")

def send_player(userId, playerName):

    player = {
        "CreatedById": userId,
        "Name": playerName,
    }

    player_url = f"{BASE_URL}/api/players"
    print(f"Sending player data to {player_url}")
    try:
        response = session.post(player_url, json=player)
        print(f"Response status code: {response.status_code}")
        if response.status_code == 201:
            print("Player created successfully!")
            print("Response data:", response.json())
        else:
            print("Failed to create player:", response.text)
    except requests.exceptions.RequestException as e:
        print(f"Request failed: {e}")


def post_match_session(data):
    session_url = f"{BASE_URL}/api/matchsessions"
    print(f"Posting match session data to {session_url}")
    try:
        payload = data
        response = session.post(session_url, json=payload)
        print(f"Response status code: {response.status_code}")
        if response.status_code == 201:
            print("Match session posted successfully!")
            print("Response data:", response.json())
        else:
            print("Failed to post match session:", response.text)
    except requests.exceptions.RequestException as e:
        print(f"Request failed: {e}")

def update_player(userId, playerName):
    playerId = 12  # Replace with actual player ID

    player = {
        "CreatedById": userId,
        "Name": playerName,
    }

    player_url = f"{BASE_URL}/api/players/{playerId}"
    print(f"Updating player data at {player_url}")
    try:
        response = session.put(player_url, json=player)
        print(f"Response status code: {response.status_code}")
        if response.status_code == 204:
            print("Player updated successfully!")
            print("Response data:", response.json())
        else:
            print("Failed to update player:", response.text)
    except requests.exceptions.RequestException as e:
        print(f"Request failed: {e}")


def get_me():
    me_url = f"{BASE_URL}/api/users/me"
    print(f"Fetching current user data from {me_url}")
    try:
        response = session.get(me_url)
        if response.status_code == 200:
            print("Current user data:", response.json())
            return response.json()
        else:
            print("Failed to fetch current user data:", response.status_code, response.text)
            return None
    except requests.exceptions.RequestException as e:
        print(f"Request failed: {e}")