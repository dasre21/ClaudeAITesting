Feature: Login
  As a registered user
  I want to log into the application
  So that I can access my account

  Background:
    Given I am on the login page

  @smoke
  Scenario: Successful login with valid credentials
    When I enter username "standard_user" and password "secret_sauce"
    And I click the login button
    Then I should be navigated to the dashboard page

  @negative
  Scenario Outline: Unsuccessful login with invalid credentials
    When I enter username "<username>" and password "<password>"
    And I click the login button
    Then I should see an error message containing "<errorMessage>"

    Examples:
      | username       | password       | errorMessage                                  |
      | invalid_user   | secret_sauce   | Username and password do not match            |
      | standard_user  | wrong_password | Username and password do not match            |
      | locked_out_user| secret_sauce   | Sorry, this user has been locked out           |
