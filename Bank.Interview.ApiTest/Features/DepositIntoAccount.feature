Feature: DepositIntoAccount
As user I can make deposits on my account

Scenario: Deposite into an account
	Given The following deposite
		| AccountId | Amount | Currency |
		| 1         | 100    | 0        |

	When the account balance is increased by "100"
	Then the acount balance which was "500" is equal to "600"
