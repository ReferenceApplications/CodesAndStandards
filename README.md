Reference Application's CodesAndStandards
=========================================

Codes, Standards and Common Objects Used the Coding World Over.
---------------------------------------------------------------

Who of us hasn't looked for lists/object representations of Countries, Currencies, States/Provinces/Territories in our various project languages and was forced to find someone's random blog of how they solved it, all the while wondering why it wasn't built in or wasn't available from the organizations who maintain the standards?

This project is devoted to three things:

* Providing robust, readily editable, community driven data stores able fulfill these recurring needs
* Code generation templates for the generation of simpler data sets and distributable source
* Advanced compiled libraries (and their source of course) for read use and dependency manager use (Nuget, CocoaPods, Maven, etc.)


Roadmap
-------

* Expand the Data Stores (currently other language support and particular AsiaPac standards needed)
* Create Templates for Multiple Languages
* Design and Implement Advanced Libraries providing simple use yet advanced features for the entities represented

Data Store Methodology
----------------------

Currently the Geographic/Country/Subdivision data stores are based in part on [ISO Standards](http://www.iso.org), [ANSI](http://www.census.gov/geo/www/ansi/statetables.html) and other IT standards.  They have been extended slightly to enable change management, optimize for storage/retrieval in computer systems, localization separate of the standards, efficient normalization and enforcement of the standards (the various organizations can't even standardize on the capitalization of International or formatting of change notices).

Countries of the World / Pays du Monde
---------------------------------------

*This is the current focus of contributors' efforts.*
Country/Dependent Territories records are currently based on the ISO-3661-1 standard along with future support of ISO-3661-2 subdivisions (states/territories/provinces etc).  Data formats and schemas are encouraged to be discussed.

*Data records are currently based in the idea of Just Enough-Point In Time (Jepit) architect.*
The world changes, countries spilt, merge, rename themselves and sometimes even pretend for a while.  Compiled code doesn't like that, but Point In Time can manage this.  Unfortunately Point In Time increases maintenance, storage, and headaches in general.  To mitigate this, DateCreated, Reason, PreviousKey, are governed by convention and need.  Changes in ISO-3166-1 numeric codes MUST be audited and accounted for.  Alpha Long Name changes are a different matter, your code better not be based on the surrogate key of "the Socialist People's Libyan Arab Jamahiriya".  Mistakes in our data, code, etc will be judged on a case by case basis with the assumption that source code and a note will suffice for the majority of cases.

Jepit Fields:
DateCreated - Audit only, currently used for reference purposes but can be utilized in templates if code is particular sensitive to change
DateEffective - The date the record became active
DateReplaced - Nullable, the date the record is replaced
DateEnded - Nullable, the date the record ceased being active.  Queries including the DateEnded will include the record as it is assumed to be active throughout its last day.
Reason - Reason for Creation or Ending.  If replaced reason can be found in the new replacement records
Key - Yep
OriginalKey - Nullable, for purposes of tracing original values.  If the need arises this can be extended to track previous key.
