# Optimal Investment Strategy

Develop a program for automatic investment decisions given a choice of profitable projects. You, as an investor, have some start-up capital, S, to invest and a portfolio of projects P in which you would like to invest. 

Your optimal strategy is to maximize the cumulative capital due to this investment.

To help them with the decision, you have information on the capital requirement for each project and the profit it’s expected to yield. You invest in these projects in any serial order you choose, maximizing your investment.

For example, if project A has a capital requirement of 3, and your current capital is 1, you can’t invest in this project. On the other hand, if the capital requirement of project B is 1, then you can invest in this project. 

Now, supposing that the project yields a profit of 3, the investor’s capital at the end of the project will be the cumulative sum of start-up capital S + the return on investment 3 (1+3=4). You can now choose to invest in Project A since the current capital has increased.

As an essential risk-mitigation measure, you would like to limit the number of projects, L; you invest in. For example, if the value of L is 2, then we need to identify the two projects that you can afford to invest in, given the capital requirements, and that yield the maximum profits.

Further, these are one-time investment opportunities; you can only invest once in a project.

Constraints:
The starting capital S can be between 1 and 10^9.
The number of project L can be between 1 and 10^5.
The portfolio size of project P can be between 1 and 10^5.
Capital requirements for a project can be between 1 and 10^9.
Profit from each project can be between 0 and 10^9
You must generate the portfolio size of P projects with a random value in Capital and Profits based on the above constraints.



Example:

Input Portfolio Size P:
5

A Random Portfolio of size five was generated.

Capital
P1 P2 P3 P4 P5
1  3  4  5  6


Profits for respective projects
1 2 3 4 5

Input initial capital S:
2

Input investment limit L:
3

Output
—-------------------
Selected Projects P1, P2, P4 with capital 1, 3, 5
Selected Profits 1, 2, 4

Optimal Investment Strategy returns = 2 + 1 + 2 + 4 = 9

Optimal Strategy maximizes your capital to = 9 
—-
