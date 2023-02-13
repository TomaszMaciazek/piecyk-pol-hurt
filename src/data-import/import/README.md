# Project structure

This module affects two parts of project:
 * .NET gRPC server ()
 * python gRPC client

**Client** read import data from file with filename pattern "import_.xslx". Parses the data to messages described  [here](https://github.com/TomaszMaciazek/piecyk-pol-hurt/tree/main/src/PiecykPolHurt/PiecykPolHurt.DataImport/Protos) in .protofile.
It does it 4 times a day (can be changed)
**Server** gets the messages and updates database according to received data. If data (date of product stock, quantity, price) does not differ from what's already in database - call to DB is not performed.

# Running

In order to run importer:
1. Run server ([this project](https://github.com/TomaszMaciazek/piecyk-pol-hurt/tree/main/src/PiecykPolHurt/PiecykPolHurt.DataImport))  
2. Run client python script `python .\data_import_scheduler.py`

Make sure, your python environment has installed packages listed [here](https://github.com/TomaszMaciazek/piecyk-pol-hurt/blob/main/src/data-import/import/requirements.txt)
