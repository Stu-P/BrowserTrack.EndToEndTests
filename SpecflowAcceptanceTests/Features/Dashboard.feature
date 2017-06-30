Feature: Dashboard
	In order to know when I should update my browsers for cross browser testing
	As a tester
	I want to see what the latest verisons are for popular browsers 

Scenario: Opening the dashboard loads a grid of browsers and current version
When I navigate to the dashboard page 
Then a grid of browsers is displayed containing
| Browser Name       | Browser Version   |
| Chrome - Windows   | Version: 43.12.25 |
| Firefox - Windows  | Version: 38.6521  |
| IE - Windows       | Version: 11.0.1   |
| Safari - OSX 10.10 | Version: 8.0.7    |

Scenario: If a version check is overdue for a browser then I am presented with alert
Given I am on the dashboard page
When I click the overdue alert icon
Then a message "Warning: version scan overdue for one or more browsers" is displayed


Scenario: User who has not signed in is unable to enable or disable browser checking state
Given I am on the dashboard page
When I click on the chrome browser tile
Then the browser switch is disabled

Scenario: After logging in a user can disable browser checking for particular browser
Given I have logged in and navigated to dashboard
When I click on the chrome browser tile
Then the browser switch is enabled

