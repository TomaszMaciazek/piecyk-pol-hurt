import threading
import time
import schedule
from grp_service import sendUpdate
import pandas as pd

def read_file():
    data = pd.read_excel('import_30_01_2023.xlsx')
    sendUpdate(data)

def run_threaded(job_func):
    job_thread = threading.Thread(target=job_func)
    job_thread.start()

schedule.every(5).seconds.do(run_threaded, read_file)

while True:
    schedule.run_pending()
    time.sleep(1)