# Rock Paper Scissors — Intern Workshop

Welcome! This is your sandbox for the Day 2 hands-on workshop. By the end of the morning you'll have written real C#, opened a real PR on GitHub, reviewed someone else's PR, and either merged your own code to `main` or approved a partner's.

The app right now plays exactly **one round** of Rock Paper Scissors against the computer and exits. That's it. The features below are your job.

---

## Prerequisites

You should already have these from Day 1's setup block:

- .NET 8 SDK (`dotnet --version` returns something starting with `8.`)
- Visual Studio 2022 (or your preferred editor — VS is what we'll use in the room)
- Git configured with your Buildertrend email
- GitHub access — you can sign in and see this repo

---

## Get it running

```bash
git clone <repo-url>
cd rps-workshop
dotnet run
```

You should see the game prompt. Type `rock`, `paper`, or `scissors` and hit enter. The game prints the result and exits. If it doesn't run, flag a facilitator — that's a setup problem, not a you problem.

---

## How we work today

You'll follow our normal Buildertrend flow on a tiny scale:

1. **Pick a feature** from the menu below. Tell the room which one so we don't all pick the same.
2. **Create a branch** off `main` using our naming convention:
   ```
   users/<your-name>/rps-<short-description>
   ```
   Example: `users/jane.doe/rps-input-validation`
3. **Commit as you go** — small, descriptive commits beat one giant one. Imperative mood:
   - Good: `Add input validation for player choice`
   - Less good: `fixed stuff`
4. **Push** your branch to GitHub.
5. **Open a PR** into `main`. Title format: `[RPS] <what you did>`. Description should include:
   - What you built
   - How to test it (one or two steps)
   - A screenshot of the game running with your feature
6. **Get a review** from your partner. Address comments, push fixes, get an approval.
7. **Approve** your partner's PR. If you have time, merge one.

---

## The Feature Menu

Pick **one** to start. If you finish, grab a second or a stretch goal.

### 1. Input validation
Right now if you type `banana` the game treats it as a loss because `banana` doesn't beat anything. Fix it. If the input isn't `rock`, `paper`, or `scissors`, tell the user and ask again. Don't crash.

**You'll touch:** `GetPlayerChoice()`. Probably a loop.

### 2. Best of N rounds
Right now the game plays one round and quits. Make it play best-of-3 (or best-of-N if you want to ask the user first). Track wins per side. Announce the overall winner at the end.

**You'll touch:** `Main()`. Probably a loop and a couple of counter variables.

### 3. Running scoreboard
After every round, print a tally like `Score — You: 2  Computer: 1  Ties: 0`. Works alongside Best of N if someone else picks that one.

**You'll touch:** `Main()` and probably a small helper method.

### 4. Refactor to enums
Today the game compares strings everywhere (`player == "rock"`). That's brittle. Replace the strings with an `enum Choice { Rock, Paper, Scissors }` and update the logic. Same behavior, cleaner code.

**You'll touch:** Most of `Program.cs`. Great way to learn how a refactor PR looks.

### 5. Stats command
Add a special input — when the user types `stats` instead of a move, print their lifetime W/L/T for this session and prompt again without counting it as a round.

**You'll touch:** `GetPlayerChoice()` and `Main()`.

### 6. Game history log
Every round, append a line to `history.txt` with the timestamp, your choice, the computer's choice, and the result. Intro to `System.IO`.

**You'll touch:** `Main()` or a new helper method. Look up `File.AppendAllText`.

### 7. Rock Paper Scissors Lizard Spock
Extend the game with two new moves. Win matrix:
- Scissors cuts Paper
- Paper covers Rock
- Rock crushes Lizard
- Lizard poisons Spock
- Spock smashes Scissors
- Scissors decapitates Lizard
- Lizard eats Paper
- Paper disproves Spock
- Spock vaporizes Rock
- Rock crushes Scissors

Bonus points if you do this after someone else has done the enum refactor — branch off their branch.

**You'll touch:** `Program.cs` widely.

### 8. Cheating computer mode
Add a `--cheat` command-line flag (look up `args` in `Main`). When set, the computer picks the move that beats whatever you played last round. First round is still random.

**You'll touch:** `Main()` and `GetComputerChoice()`. Probably needs to remember the last player choice.

---

## Stretch Goals (after your first PR is reviewed)

- Pick a second feature
- Pair with someone whose feature complements yours and combine them in a new branch
- Add a small unit test (look up `dotnet new xunit` — facilitators can point the way)
- Improve the UI: colored output for win/loss/tie using `Console.ForegroundColor`
- Write a `README` section documenting the feature you added

---

## If you get stuck

- Try it for ~10 minutes
- Ask your neighbor
- Then flag a facilitator
- "I tried X, expected Y, got Z" gets the fastest help

Have fun. You're going to be doing this for real in a couple weeks.
