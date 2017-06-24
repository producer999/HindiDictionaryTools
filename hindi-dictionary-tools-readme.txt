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
https://stackoverflow.com/questions/23144519/gridview-with-2-columns-fill-width
https://social.msdn.microsoft.com/Forums/sqlserver/en-US/f02f3880-f8b8-4e35-95bb-2646ce2e5d0c/uwp-binding-listviewselecteditem-with-xbind-twoway-mode-causes-stackoverflowexception?forum=wpdevelop
https://codeblog.jonskeet.uk/2015/01/30/clean-event-handlers-invocation-with-c-6/

Coming Soon:

****when pressing Enter in def entry fields, activate the Add Contact Button
****store alternate definitions, alternate forms and examples as JSON in the databse
****add stop button during import that cancels the import where it is
****add timer to show how long an import took
****add button to get all google translations (maybe)
****find way to bind PartsOfSpeech enum to combobox ItemSource without using code behind
****find out if you can use x:Bind/x:DataType for items in DataGrid List
****replace code behind AddNewTranslation_Click event handler with MVVM implementation
****try to improve import speed for main translation file (or break up file into smaller pieces)
****fix listview binding on top right (doesnt show empty values for null current translation)
****add a textblock that displayes the numbers of search results after searching
****make call to automatically UpdateTranslation to databse when navigating away from a translation
****put upper left section in a ScrollViewer
****improve load time when many translations are in the database
****get database to update all changes made automatially (add button?, do automatically?)


v 00.00.10 6/23/2017
-made search string automatically filter the list using two way binding on a property which sets the DataGrid filter
-added Noun (not M or F) as a Part of Speech
-ComboBox x:Bind is now working TwoWay, stack overflow was casue by infinite loop of property keep setting itself
	-needed to set a check on the PartOfSpeech Property on the Translation to only update if value != currentVal
-current translation automatically update in database when new POS selection is made
-update Parser to account for different types of verbs
-added parsing for Independent Clause, Adjective phrase, Adverb phrase, Preposition (=postposition)
-added new parsing information to improve part of speech detection


v 00.00.09 6/22/2017


-bind Update, Delete and google Refresh buttons IsEnable bound to IsCurrentTranslationSelected property
-make buttons look different for enabled and disabled and fix colors and text
-added search box
-implemented simple search functionality with a search button using DataGrid.SetFilter
-make it so you can go back to showing all definitions by searching empty string


v 00.00.08 6/21/2017

STATUS:
-update button works
-import button works
-clear database button works

-parse the part of speech data to look for N(F) or N(M) and verb type
-fix DataGrid does not populating/updating after importing from text
	-calling Dictionary = GetAllTranslations does not update the listview binding
	-manually raised PropertyChagned event on Dictionary after import


v 00.00.07 6/20/2017
-added Add Translation button functionality in code behind (need to find a way to do with Command or x:Bind)
-fix NullReferenceException when clicking a translation, updating a field, then selecting the same one from list
	-its setting SelectedItem to null in translation list, THEN trying to update the binding
	-diabled the ability to deselect an item in the list
-implemented basic functionality to update edits to current translation in databse when selecting another one in list
-implemented google translation refresh button for current translation
-added delete current translation button functionality
-added clear database button
-added import from file button and removed automatic file import when initializing application without a database 
-added some error checking to prevent null references
-when adding definitions, only refresh the newest one instead of refreshing the entire dictionary (prevent flikr)
-adjusted TranslationParser to account for the longer list of translations and tested importing from big file


v 00.00.06 6/15/2017

-added basic update translation button using ICommand
-update code to use complied bindings instead


v 00.00.05 6/14/2017
-bound part of speech combobox ItemSource to partsofspeech enumeration
-got current translation part of speech to populate in combobox selected item and bind both ways
-fixed textbox and grid sizing on initial load


v 0.00.04 6/7/2017
-added fields and buttons for Translation detail editing and viewing
-add gridsplitter between sections on the top row
-two way data binding working between the current details and the dictionary list view


v 0.00.03 6/6/2017
-added a GridSplitter (UWP Toolkit) between the entry fields and the ListView
-fixed Data Context of the Page to the HindiDictionary class
-Bound the ListView to the HindiDictionary's Dictionary collection and bound the Items to the properties of the Translations in the collection
-created listview styling
-replaced listview with MyToolkit.Extended DataGrid and tested functionality
-customized functionality and styling of DataGrid
-make it so the first time you run the app it creates the database from the text file if databse is empty
-add a textblock up top to display the number of translations in the dictionary
-fix incorrect converting of part of speech


v 0.00.02 6/5/2017
-redesigned view to show new entry, current selected data and the list in one page
-added StaticResources for commonly used colors
-added Style Definition for TextBox to change the header text color


v 0.00.01 6/3/2017

-verified basic functionality derived from the DictionaryTools project
-refactored code to follow more of an MVVM design
-built data Classes and a HindiDictionary "view model"
-built helper classes for database access and google translate api access