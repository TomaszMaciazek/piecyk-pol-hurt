import grpc
import greet_pb2
import greet_pb2_grpc
from data_import_stub import send

def sendUpdate(data):
    named_request = greet_pb2.ProductUpdateRequest()
    for row in data.itertuples():
        messageRecord = named_request.productUpdate.add()
        messageRecord.productType = row.PRODUCT_CODE
        messageRecord.quantity = row.QUANTITY
        messageRecord.sendPoint = row.SENDPOINT_CODE
        messageRecord.date = str(row.DATE)
        messageRecord.price = row.PRICE
    send(named_request)