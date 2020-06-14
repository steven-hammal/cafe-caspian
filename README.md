# cafe-caspian
My attempt at the ever popular Caspian Cafe challenge.

## What it does
Given a sequence of food items as cli arguments, the application calculates the 
total price from it's menu and applies a service charge as defined in the requirements.

## Notes
The requirements are written in an ambiguous way (intentionally?) the service charge
logic could be taken to mean that the max charge of £20 only applies to hot food and
any order with only cold food can have a limitless charge.  This should be a good 
topic of discussion in a follow up interview but no one has yet mentioned it.

The task states that the simplest solution should be implemented at each stage and I 
would regard reading in the menu from config and setting up some form of DI to be simple
as they give you so much in terms of demonstrating SOLID principles and enabling TDD.

I've defined the models under a Domain project but technically it isn't a domain project
since there is no data access layer where domain objects live.

Notice I haven't just copy and pasted the task instructions themselves here.

## Stretch Goals
I would like to have a go at doing this using the State Pattern.  In this implementation
the business logic would all be properties of the objects themselves.  For example, all 
configurations of the service charge rules would be represented as a different polymorph 
of some base order class with a GetServiceCharge member.  It would look strange but I've 
wanted to have a go at it for a while. 


