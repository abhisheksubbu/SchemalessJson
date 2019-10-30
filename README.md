# SchemalessJson
### This project is a POC for demonstrating data parsing from complex objects to dynamic json & vice-versa

1. These are ideal reference architecture to understand how complex data can be parsed dynamically & saved into json format. 
2. This technique eliminates the need of having a normalized structure but want the data be saved in normalized databases.

### Scenarios where I have used this as a reference
* A form collects different sets of data like Text, Choices, File or List of Files
* We want to save this form data in the form record in SQL database
* Client doesn't prefer NoSQL because they are using SQL databases. So, the design needs to use normalization for storing de-normalized data.
* The design should provide facility for reading de-normalized data and able to understand how to present useful properties in a summary page.
