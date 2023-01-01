U can use to understand what include in jwt token https://jwt.io/
for an example token 
eyJhbGciOiJIUzM4NCIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IjU1YjlkOTI1LTVhYjgtNDdhYi1mOTQ0LTA4ZGFlMjY3N2U5MiIsImVtYWlsIjoiYWhhb3pkZUBkZmRzLmNvbSIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL25hbWUiOiJjYWhhdGF5IiwibmJmIjoxNjcxNjk4NzExLCJleHAiOjE2NzE3Mjg3MTEsImlzcyI6Ind3dy5jYWhhdGF5LmNvbSIsImF1ZCI6Ind3dy5jYWhhdGF5LmNvbSJ9.feMt7wlFLBFP4LrtryYg7bnVgwkiTr3Z1SS3JM9pGPuyp6uI0w8v20aTl7zO_62h


u can use postman scripts in solution.
u have to use in first step the controller name is auth/companyfirstregister.
this process will create a company-companybranch-user 
you will use this user so keep email and password record in your notes.

second step is Login attempt
use ur email and password 
login auth will save your token it comes from endpoint response.
(in postman we write a little code for save your token in variable
so u can use the same token in every endpoint which needs authrozitaoin)



tests are not written yet..


in this project ,
we can sell this product many companies so ,
every entity has CompanyId, when user login we will look the companyId
so users connected to branch-company-product-productUnit-offer-offerrow-user etc entity's.
in controller we dont use companyId because we make a plan like request comes from UI so they have the companyId
so they will make a filter for theirselves.
project will continue to improvement.


first step for improvement is 
make a filter in every controller endpoint with a companyId it comes from token. Dont get any parameter like companyId.

