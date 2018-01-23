Feature: HorseBetting
	As a WilliamHILL betting site user	
	In order to successfully add a bet 
	I want to be able to add a horse racing bet to "bet slip" and add a stake

@UI
Scenario: Add a future horse racing bet to the "bet slip" and add a stake
	Given I am in WilliamHill future horse betting site page
	And I select the horse with name as "2018 The Everest 1200m (G1) (Before Noms - All In Betting)" from feature Race
	When I select my Racecard as Runner:"Redzel"
	Then I could add a stake as "10.5" to the Bet Slip and confirm:
	



