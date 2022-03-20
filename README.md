# GridLn

GridLn is a compiler that compiles GDI+ graphics instructions in a human-readable form, to an image

## Features

### Headers:

> Headers start with a "!", they provide certain flags and values for the
compiler.

* <pre>WID INT   : Sets the width ( in pixels ) of the output file
* <pre>HEI INT   : Sets the height ( in pixels ) of the output file
* <pre>BGC R G B : Sets the background color to an RGB value.
* <pre>OUT STR   : Sets the output file ( not relative )

### Functions:

> Functions are GDI+ graphics commands, they draw to the file in sequential
order.

* <pre>MOV X Y                     : Moves the drawing cursor to X and Y positions respectively.</pre>
* <pre>LIN X Y                     : Draws a line from the cursor position to the X and Y positions respectively.</pre>
* <pre>LMV X Y                     : Draws a line from the cursor position to the X and Y positions respectively, then moves the cursor to the end of the line.</pre>
* <pre>CLR R G B                   : Sets the color of the cursor to an RGB value.</pre>
* <pre>ARC W H stA swA             : Draws an arc with the width (W), height (H), startAngle (stA) and sweepAngle (swA).</pre>
* <pre>BEZ X1 Y1 X2 Y2 X3 Y3 X4 Y4 : Draws a bezier with the specified X1-4 and Y1-4 values.</pre>
* <pre>CCU P[X Y]                  : Draws a closed curve with the specified list of points (P), values should be entered in a even X Y X Y X Y... fashion.</pre>
* <pre>CUR P[X Y]                  : Draws a curve with the specified list of points (P), values should be entered in a even X Y X Y X Y... fashion.</pre>
* <pre>ELL W H                     : Draws an elipse with the width (W) and height (H) values.</pre>
* <pre>PIE W H stA swA             : Draws a pie with the width (W), height (H), startAngle (stA) and sweepAngle (swA).</pre>
* <pre>POL P[X Y]                  : Draws a polygon with the specified points (P).</pre>
* <pre>REC W H                     : Draws a rectangle with the specified width (W) and height (H).</pre>
* <pre>TXT T                       : Draws a string of text (T) in the default system font.</pre>
* <pre>FCC P[X Y]                  : Draws a filled closed curve with the specified list of points (P), values should be entered in a even X Y X Y X Y... fashion.</pre>
* <pre>FEL W H                     : Draws a filled elipse with the width (W) and height (H) values.</pre>
* <pre>FPI W H stA swA             : Draws a filled pie with the width (W), height (H), startAngle (stA) and sweepAngle (swA).</pre>
* <pre>FPO P[X Y]                  : Draws a filled polygon with the specified points (P).</pre>
* <pre>FRE W H                     : Draws a filled rectangle with the specified width (W) and height (H).</pre>
* <pre>PIX                         : Draws a pixel at the X and Y of the cursor.</pre>
* <pre>EOF                         : Ends the file and writes to the output.</pre>

### Comments:

> Comments start with a "~" and mean nothing.
