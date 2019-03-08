Feature: HappyPathTest
		Background: Set Test Data
		* Set test data

@mytag
Scenario: Test Smoke Email
	Given Login to the mail.ru as a valid user
	Then I verify that user is signed in
	When I create a mail
	And I navigate to DraftsPage
	And I open Draft Email on DraftsPage
	Then Verify the draft content addressee, subject and body – should be the same
	When I send the mail
	And I navigate to DraftsPage and verify, that the mail disappeared from ‘Drafts’ folder
	And I navigate to SentPage
	And I verify, that the mail presents in ‘Sent’ folder
	Then I log out



	
