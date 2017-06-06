Hindi Dictionary Tools (UWP)
______________________

Description:

Copyright 2017 sourcEleven

This application will assist in building and editing the Hindi Translation Dictionary that will be used inside of the main "PDF with Translation Tools" application. 

The Tools app creates a database of Hindi Translations based off of User Input, Imported Translation Data (formatted text file) and Cloud Translation APIs (such as Google Translate). It combines all of this data into one database that will be used to identify any available data on a particular Hindi word or short phrase. Once integrated into the PDF app, this will be used in interesting ways to assist in reading literature in a foreign language (Hindi in this case).

References:

http://bsubramanyamraju.blogspot.in/2016/12/windows-10-uwp-sqlite-how-to-store-data.html
http://www.cfilt.iitb.ac.in/~hdict/webinterface_user/index.php
http://www.c-sharpcorner.com/UploadFile/2b876a/consume-web-service-using-httpclient-to-post-and-get-json-da/


Coming Soon:

****when adding definitions, only refresh the newest one instead of refreshing the entire dictionary (prevent flikr)
****when pressing Enter in def entry fields, activate the Add Contact Button
****do a check on Add Contact that the Word is not in english, devanagari only
****store alternate definitions, alternate forms and examples as JSON in the databse
****add stop button during import that cancels the import where it is
****add timer to show how long an import took
****add a tool tip when mouse over the definition in the list to show all details
****add button to get all google translations (maybe)


v 0.00.02 6/5/2017
-redesigned view to show new entry, current selected data and the list in one page
-added StaticResources for commonly used colors
-added Style Definition for TextBox to change the header text color


v 0.00.01 6/3/2017

-verified basic functionality derived from the DictionaryTools project
-refactored code to follow more of an MVVM design
-built data Classes and a HindiDictionary "view model"
-built helper classes for database access and google translate api access