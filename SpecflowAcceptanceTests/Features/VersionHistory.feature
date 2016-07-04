Feature: VersionHistory
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers


Scenario: Opening the Version History page displays a list of version changes
Given I am on the dashboard page 
When I click Version History from the menu
Then a list of version changes is displayed
| VersionChange                            |
| 06/11/2015 - Changed to 8.0.7 from 8.0.5 |
| 05/04/2015 - Changed to 8.0.5 from 8.0.2 |
| 01/04/2015 - Changed to 43.2 from 43.1   |
