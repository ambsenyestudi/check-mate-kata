# Checkmate kata 

If you are on IT business chances are you hear of this Agile thing. Iterations and small feedback loops are of the essence, bringing value to the table otherwise know as deploying to production quality code in the shortes shortest of time.

Another not so clear notion is vertical slicing. This means you need to complete a system-wide feature as small as possible that puts you one step closer to the business value. Remember we are all business

## The check mate detector

Our small startup Play it smart has thought of a nice application. An microservice that detects checkmates in a chess game, this will make us millions so let's slice that problem.

# User stories

* Given that a valid movement is submitted if the start square contains a piece then a movement can be made
* Given that a pawn moves when a king at different is at killing range then check is detected
* Given that a bishop moves when a king at different is at killing range check is detected

For now let's go with colum row of origin and colum row end as a valid move.
