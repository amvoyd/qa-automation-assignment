Feature: API and UI interaction flow

  Scenario: API + UI interaction flow
    Given a GET request to "https://api.restful-api.dev/objects" is sent
    And the user navigates to "https://the-internet.herokuapp.com/"
    And the user opens Slow Resources and logs the response
    And the user opens Dynamic Loading Example 1
    And the user clicks the Start button
    And the user saves the loaded result text after loading
    When the user opens Form Authentication
    And the user inputs the loaded result text as username
    And the user inputs the first object name from the API response as password
    And the user clicks on the Login button
    Then the error message "Your username is invalid!" should appear