Feature: TestDeleteEmail
	Background: Set Test Data
		* Set test data

@mytag
Scenario: Test Delete Email
	Given Login to the mail.ru as a valid user
	Then I verify that user is signed in
	When I create a mail
	And I send the mail
	And I navigate to SentPage
	And I verify, that the mail presents in ‘Sent’ folder
	And I delete email dragging to the trash bin
	And I navigate to recycle bin
	Then Verify, that the mail presents in ‘Recycle bin’ folder	
