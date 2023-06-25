Feature: WithdrawFromAccount

As user I can make withdraw from my account

Scenario: Withdraw from an account
	Given The following withdraw
		| AccountId | Amount | Currency |
		| 1         | 200    | 0        |

	When the account balance is reduced by "200"
	Then the new bank balance is "300" compared to "500" previously
