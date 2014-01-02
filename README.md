fileOrganiser
=============

file organiser for grouping files in mmm yyyy formatted folders, by created date or taken date if photos

Initial Use:
============

180,000 deepscan recovered files of varying types. All recovered to one directory with sequential numbering. 

EXIF information intact for images.

Requirement to categorise recovered files of specific type in to new directory structure, by folders structured
MMM YYYY. 

If the file is an image with EXIF datetaken attribute then the image should be organised using this date. Otherwise
Use the datecreated attribute.

Files will store with file name format ddmmyyyhhmmss and original suffix. If this already exists, iterate and 
append _n where n is next available sequence.


User Input
==========

Gui prompting for source and destination directories

Constaints
==========

Predefined list of valid file types





