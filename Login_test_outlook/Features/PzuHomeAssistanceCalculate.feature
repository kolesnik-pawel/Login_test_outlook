Feature: PzuHomeAssistanceCalculate

A short summary of the feature

@tag1
Scenario: fill form
	Given Open Browser and go to page 'https://moje.pzu.pl/pzu/home'
	#When I check then page loaded until '8000' miliseconds
	Given Wait permamently for '5' seconds
	When Find '//*[@id = 'terms-page1']/div[2]/a[2]' element using XPath and save as 'cookeebutton'
	Then I Click at 'cookeebutton' button
	Given Wait permamently for '2' seconds
	When Check then popup at '//*[@id ='exit-modal-content']/div/a' are visible And close it
	When Find '//*[@class ='pzu-tile-set__wrapper ']/div/div/label' element using XPath select type and try Click it
	Given Wait permamently for '2' seconds
	When Check then popup at '//*[@id ='exit-modal-content']/div/a' are visible And close it
	
	When Find '//select/option[@value ='PL_LU']' element using XPath select type and try Click it
	#When Find '//*[@value ='apartment']' element using XPath and save as 'home2'
	#Given Wait permamently for '2' seconds

	Given Wait permamently for '16' seconds
	
