import threading
import time
import schedule
from grp_service import sendUpdate
import pandas as pd
from glob import glob

def read_file():
    for f in glob('import_*.xlsx'):
        data = pd.read_excel(f)
        break
    sendUpdate(data)

read_file()

def run_threaded(job_func):
    job_thread = threading.Thread(target=job_func)
    job_thread.start()

schedule.every(5).seconds.do(run_threaded, read_file)

while True:
    schedule.run_pending()
    time.sleep(1)