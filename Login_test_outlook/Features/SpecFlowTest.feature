Feature: SpecFlowTest

Negative tests to login outlook.live.com 

Scenario: LogIn scenario
	Given Open Browser and go to page 'https://outlook.live.com' 
	When Find 'html/body/header/div/aside/div/nav/ul/li[2]/a' element using XPath and save
	Then I Click at button
	When Find 'loginfmt' element using Name and save
	Then I fill email input using 'Test_mail@exchange.com' email
	And Find 'idSIButton9' element using Id and save
	And I Click at button
	When I check then page loaded until '8000' miliseconds
	When Find 'usernameError' element using Id and save as 'errorMessage'
	Then I Check at the 'errorMessage' object contains 'That Microsoft account doesn't exist. Enter a different account'

Scenario: LogIn scenario2
	Given Open Browser and go to page 'https://outlook.live.com' 
	When Find 'html/body/header/div/aside/div/nav/ul/li[2]/a' element using XPath and save as 'SingIn'
	Then I Click at 'SingIn' button
	When Find 'loginfmt' element using Name and save 
	Then I fill email input using 'Test_mail@exchange.com' email
	And Find 'idSIButton9' element using Id and save as 'NextButton'
	And I Click at 'NextButton' button
	When I check then page loaded until '8000' miliseconds
	When Find 'usernameError' element using Id and save as 'errorMessage'
	Then I Check at the 'errorMessage' object contains 'That Microsoft account doesn't exist. Enter a different account'

Scenario Outline: LogIn scenario3
	Given Open Browser and go to page '<pageURL>' 
	When Find '<SingInButton>' element using XPath and save as '<SaveSingIN>'
	Then I Click at '<SaveSingIN>' button
	When Find 'loginfmt' element using Name and save 
	Then I fill email input using '<TestMail>' email
	And Find '<NextButton>' element using Id and save as '<SaveNextButton>'
	Then I Click at '<SaveNextButton>' button
	When I check then page loaded until '8000' miliseconds
	When Find 'usernameError' element using Id and save as 'errorMessage'
	Then I Check at the 'errorMessage' object contains '<ErrorMessageContains>'

	Examples: 
	| pageURL                  | SingInButton                                  | SaveSingIN | TestMail               | NextButton  | SaveNextButton | ErrorMessageContains                                            |
	| https://outlook.live.com | html/body/header/div/aside/div/nav/ul/li[2]/a | SingIn     | Test_mail@exchange.com | idSIButton9 | NextButton     | That Microsoft account doesn't exist. Enter a different account |
	| https://outlook.live.com | html/body/header/div/aside/div/nav/ul/li[2]/a | SingIn2    | Test_m@exchange.com    | idSIButton9 | NextButton     | That Microsoft account doesn't exist. Enter a different account |
	| https://outlook.live.com | html/body/header/div/aside/div/nav/ul/li[2]/a | SingIn3    | iTechArt@exchange.com  | idSIButton9 | NextButton     | That Microsoft account doesn't exist. Enter a different account |