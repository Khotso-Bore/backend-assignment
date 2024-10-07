# Assignment Notes

While working on this assignment i had a fun time and found rather simple at first but it was i got to load tessting my rabbit application things took a turn.

## Start-up

I first began modeling out my Entities ( Player,Game,Provider and Casinowagers) figuring that when inserting wagers into the database that i woukd have ensure referential integrtity and that wagers do consist of data that does exist in the database but I later realised that shouldn't be the case as that would require the data for those tables to be preprepared and not randomly generated. So I removed all of them save for Casino Wagers.

I went with a standard N-tier architecture for my solution. Consisting of a Domain, Infrastructure, BLL (Business Logic Layer) and Apllication Layers.I also made use of UnitOfWork and Repository patterns, figuring i might be working with multiple repositories at once and having shared instance of the DbContext among them and saving related changes would be nice, but this woulf not be tha case.

I then procceded to implement my business logic in my serviices and inject them into my controller. With producer and consumer clients Set up as well.

## NBomber

Here's where it gets interesting. When initially testing out my application i used used to my Api to insert wagers and it worked perfectly when publising and consuming and inserting the data.

I began to test with Nbomber and immediately things began to break. I noticed that my publisher wouldn't work consitently or just fail outright and the consumer was having trouble inserting the wager entries.

Thinking it was simply an implenentation issue, As it worked fine when maing a single. I spent countless hours trying to figure out where I misstepped, what propert or setting could I add to make this work.

When Nbomber starts up it makes 500 concurrent request to my endpoint every 2 seconds until a total of 7000 requests had been made. Only then did i realise that what was actually happening. My RabbitMq producer would recive these requests and try and open 500 indiviaul connections in a span of 2 seconds, each trying to publish to the queue. It was then i realised that i needed to implement bulk publishing and doing so work out amazingly. My pubshisher was working again!!!

However my Consumer still was is working adequtly. As inserting such a large number of items concurrectly does seems to pose a challange.

# Thoughts

The test was fun overall. Learned a lot from it, I just wish i realised what to do earlier then i would've had a better system. There are still some things i would've liked to implement such as:

1. logging: would've been useful to capture logs at different points for better debugging
2. Deduplication: At these volumes i imagine duplication can occur often, perhaps i could've made a method to check against existing Id's and mark found ones as duplicates to be processed later.
3. Stored procedure: a stored procedure to check again exsting items would've been nice
4. a cleaner system but time was against me

overall good time. hope you enjoy marking my code.
