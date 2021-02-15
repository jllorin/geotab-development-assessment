# Walkthrough of changes I've made

## Overview

I fixed the code partly so that I would know the basic gist of the code but since I think there will be a lot of changes that I know will make, it will be much easier for me just to create a new project based on the old code.

## Instructions on how to compile and run

Make sure you have .net sdk 3.1 installed on your machine.

1. git clone https://github.com/jllorin/geotab-development-assessment.git
2. cd geotab-development-assessment
3. git pull origin feature/upgrade-joke
4. cd JokeCreator
5. dotnet run
6. open another console
7. cd geotab-development-assessment\joke-creator
8. npm install
9. npm run start
10. open chrome and go to [http://localhost:3000]

## Architecture

I created a full stack application where I used dotNet Core as my backend and React, Javascript, HTML, CSS as my front-end. 

For my backend, I used the microservices, domain-driven, service-repository architecture. In my experience in microservices, I like the domain-driven approach with service repository pattern where you divide the problem at hand into different domains. Inside the domain, you add services, repositories and controllers as needed. There are 2 domains that I saw, one is the joke and the other is the name of the person. Having this architecture limit the number of models you need to create as compared to the data layer-business layer-controller design where you have models for every layer. I find that monolith benefit from the data layer/business layer but not microservices because microservices generally are small.

For my front-end, I used React, React Material UI framework, styled components to have a nice UI for the user. 

Unit testing are both done in the backend and frontend (although not as complete).

## Usability & UX

I've added visual components to the different filters and questions you ask to the user. It asks for a category first and by default it's blank and how many jokes do you want. I limit to 1-9 as per the business requirement. The random name was asked and pulled in right away if they answer yes and displayed to the user. A submit button is clicked or Enter button is pressed to generate the jokes. I believe it's a significant improvement from the old one and cleaner too.

## Improvements I Made

- I created models for the Joke and Person to make it statically typed so that the object schema is preserved and it is what you expected in the back-end. 
- I used dependency injection, so you can inject like logger, HttpClient to places you will need it.
- I used interfaces and used that instead of concrete classes so I can substitute it with mocks on unit testing.
- Rather than working on a string when I got the feed to replace Chuck Norris, I used an object and used Replace instead of indexof so I can change all Chuck's names instead of just one Chuck if Chuck's name appears multiple times.
- I used IHttpClientFactory instead of instantiating a new HttpClient whenever you need it. There is a bug I believe when you do instantiate HttpClient directly.
- I made the code more readable I believe.
- I used dependency injection (singleton/transient) to create my classes instead of static classes. I can use singleton or transient if necessary.
- I fixed the bugs.
- UI is better

## Reliability & Quality

Service-Repository patterns are easy to test that's why they are widely used. I've used NUnit and NSubstitute to do my unit testing. I've added unit testing to some of my controller and service classes in the backend. It's on a different project but same solution. Due to time constraints, I've added only some unit testing.

I've used react-testing-library, jest, enzyme, mocha in my front end and tested my useState hook and tested that some components are in the document. Again due to time constraints, it's not nowhere comes close to what is needed. 

I've added data annotations too in the backend and do a ModelState.IsValid in my api controllers to validate the model that is being passed to my API endpoint. Validations are done that No Of jokes is from 1-9, category, firstname and lastname is not a null or is not missing from the model being passed to the API. 

I inject my logger to whatever layer I need. Exception Handlers are added on the repository but I tend to catch most of exceptions in the controllers and log it in a logger. I log both the error and stack trace in there. I just use whatever is the default logger used by dotNet which is Event log.

As for exceptions in the front end, react has an Error Boundary component that I used to catch all React errors.

## Future Maintenance & Extension

- I need to add more unit testing if this will be a product both in the front end and back end. 

- Need to clean up the code in the front end still to break them more into smaller components.

- I need to add a standard way of logging errors and exceptions and a new logger provider better than just writing in the event log. 

