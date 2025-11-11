Paul McCourt EnergyCAP technical interview

Part 1-
 Although I told you that I didn’t know anything about API’s I took this as an opportunity to learn and try my hand at Part 1. 
    1. I watched Youtube videos about how REST API’s work and scanned the ENERGYCAP documentation.
    2. Because I was under the impression that I could write REST calls from any language I first attempted writing a bash script using curl to do the operations.
    3. After failing at this, I utilized the C# SDK to attempt the challenge where I was at least able to get data back from my calls.
    6. The wall I hit- I was able to list all the vendors and all the fields from all the vendors by connecting to the vendor endpoint. I wasn't able to find the proper endpoint which would allow me to see how many unique accounts where attached to a given vendor. I tried finding a vendorID in the account table and tried to find the amount of unique accounts via the bills which connected with vendors. 
    7. Having hit a wall if I was able to find the connection between accounts and vendors via the correct endpoint here is what I would do.
        a. I would find the number of unique accounts associated with each vendor probably using an array
        b. I would sort that list to find the 6th highest
        c. I would update/PUT the website field in the vendor table to be the website to chess.com
            c2. I really enjoy the game of chess and how it teaches me qualities such as focus, strategy, patience, pattern recognition, emotional regulation, time management, etc.. I really enjoy it. 

Part 2- Answers to Part 2
    1 How many and which accounts are in the CSV file but not the Excel file?
        a. 9 accounts,
            b. Account_ID	Processing_Partner
            c. 0791910000	FDG
            d. 7602910000	FDG
            e. 361910000	FDG
            f. 4277710000	FDG
            g. 6791910000	FDG
            h. 1965048607	FDG
            i. 3827552344	FDG
            j. 2349143602	FDG
            k. 9381910000	FDG
    2 How many and which accounts are in the Excel file but not the CSV file?
        a. There are no accounts in the excel file that are not in the CSV
    3 How many unique accounts are in the Client Bill Info spreadsheet? 
        a. 31 unique accounts

How long each portion took me
    -Part 1- Part 1 took me over 8 hours of watching Youtube videos, asking AI to explain things, digging through documentation, trying a bash variation and a C# SDK variation. 
        -I chose to take this on out of curiosity and as a challenge having no prior experience with API's. 
    -Part 2- took me 15 minutes to import the data and to run a few queries

Did I use AI tools?
    Yes, I used AI tools on Part 1 
        1) To educate me
            -REST API's 
            -HTTP calls
            -C# SDK and how it worked
            -To generate code blocks so I could learn how C# SDK worked piece by piece
            -Pagination
        2) To generate code blocks for C#
            -Given the limitations of time I had to figure out this problem, I decided to utilize AI to write code blocks
            -I haven't coded C++ for awhile so AI was very useful to be able to generate code blocks which I could then study to understand what was going on.
            -AI was also useful for diagnosing error codes so I could debug quicker.
