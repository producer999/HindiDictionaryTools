Hindi Dictionary Tools (UWP)
______________________

Description:

Copyright 2017 sourcEleven

This application will assist in building and editing the Hindi Translation Dictionary that will be used inside of the main "PDF with Translation Tools" application. 

The Tools app creates a database of Hindi Translations based off of User Input, Imported Translation Data (formatted text file) and Cloud Translation APIs (such as Google Translate). It combines all of this data into one database that will be used to identify any available data on a particular Hindi word or short phrase. Once integrated into the PDF app, this will be used in interesting ways to assist in reading literature in a foreign language (Hindi in this case).

References:

https://github.com/producer999/HindiSuggestBox
http://bsubramanyamraju.blogspot.in/2016/12/windows-10-uwp-sqlite-how-to-store-data.html
http://www.cfilt.iitb.ac.in/~hdict/webinterface_user/index.php
http://www.c-sharpcorner.com/UploadFile/2b876a/consume-web-service-using-httpclient-to-post-and-get-json-da/
https://stackoverflow.com/questions/23144519/gridview-with-2-columns-fill-width
https://social.msdn.microsoft.com/Forums/sqlserver/en-US/f02f3880-f8b8-4e35-95bb-2646ce2e5d0c/uwp-binding-listviewselecteditem-with-xbind-twoway-mode-causes-stackoverflowexception?forum=wpdevelop
https://codeblog.jonskeet.uk/2015/01/30/clean-event-handlers-invocation-with-c-6/

Coming Soon:


****add stop button during import that cancels the import where it is
****add timer to show how long an import took
****add a textblock that displayes the numbers of search results after searching
****add a "Favorite" or "starred" translation attribute to the translation itself that can be used to promote it

XXXX fix copy paste from pdf doesnt show a hindi font (problem with font, not app, may be unfixable)


v 00.08.00 9/18/2017
-added IME/transliteration-to-hindi-script support using HindiSuggestBox Extension Class Library


v 00.07.03 7/11/2017
-fixed bug in release build where a call to GetFileAsync to check for database existence would run forever
	-had to change file check to use FileInfo object and Exists property (non-async)
-added an Import Database button to load existing databases for editing, instead of copying database from App folder
-add Save Database As button to make a copy of the database on the desktop (or chosen location)


v 00.07.00 7/10/2017
-add splash screen


v 00.06.00 7/5/2017
-fix pressing Enter in def/examples/alt trans/alt forms entry fields, activate the Add Button using KeyUp handler


v 00.05.00 7/4/2017
-removed spell checking from autosuggestbox


v 00.03.00 7/1/2017
-finished designing the look of the Examples editor ListView
-examples listview now functional to update the examples proerty on the Current Translation
	-had to write a helper function in the hindidictionary class to "convert back" the collection to the JSON
	-future: figure out how to get this to happen automatically using the converter class
-adjusted alt trans and alt forms converters to use the observable collection return type
-checked for null values on all converters
-fix the header text titles
-added the Alt Translations editor
-fixed example editor items template so that long sentences dont overlap the delete button
-changed alt translation editor to use a GridView instead of listview
-changed alt trans gridview to use a WrapPanel (uwp toolkit) as its itemspaneltemplate to support variable width items
-make examples, alt trans and alt forms textboxes disables when currenttranslation is null
-make listviews and grid views disabled or hidden when current trans is null
-set maxheight on examples editor listview
-fixed GridView was showing even with no items, by giving it a Width it got some height (why?), used MaxWidth instead
-added clear button to example/alttrans/altform editors
-added Alt Forms editor


v 00.01.04 6/30/2017
-changed Examples converter to convert from JSON to List<string>
-Created listview with example and X button for deleting element from the list (button not implemented yet)
-added entry textbox and Add button for adding new examples
-styled and positioned examples elements


v 00.01.03 6/29/2017
-store alternate definitions, alternate forms and examples as JSON in the databse
-added examples, altforms and alt translations JSON wrapper objects
-added examples, altforms and alt translations converter classes (convert from JSON to string, convertback str to JSON)
-changed translation data parser to import examples from files as JSON with string array in it
-added examples field and tested with 1 way binding (converback not done yet)


v 00.01.02 6/28/2017
-added classes for alttrans, altforms and examples JSON


v 00.01.01 6/27/2017
-switched from using the MyToolKit.Extended.DataGrid to the SyncFusion SfDataGrid, this fixed:
	-Data Virtualization: 136,000+ translations now load in about 6 seconds
	-Data Virtualization: adding 10,000+ translations to a large database no longer crashes/hangs the app
-added the following features to SfDataGrid: search, style/colors, sort


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