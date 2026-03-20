# QA Automation Assignment

This end-to-end automation project using:
- C#
- .NET 10
- NUnit
- Reqnroll
- Playwright

# Scenario covered
API + UI interaction flow:
1. Send GET request to `https://api.restful-api.dev/objects`
2. Save the first object name
3. Open `https://the-internet.herokuapp.com/`
4. Open Slow Resources and log the response status
5. Open Dynamic Loading Example 1
6. Click Start and wait for the result text
7. Save the loaded text
8. Open Form Authentication
9. Use loaded text as username
10. Use first API object name as password
11. Click Login
12. Verify error message: `Your username is invalid!`

# Prerequisites
- .NET 10 SDK
- PowerShell
- Internet connection
>>>>>>> bbbf378 (Add gitignore)
