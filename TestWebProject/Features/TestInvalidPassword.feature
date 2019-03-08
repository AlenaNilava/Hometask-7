Feature: TestInvalidPassword
 Background: Set Test Data for the feaure
	* Set test data

  Scenario: Test invalid password
	Given Login to the mail.ru as an invalid user
	Then Verify, that red text message appears for invalid login
	Given Login to the mail.ru as a valid user
