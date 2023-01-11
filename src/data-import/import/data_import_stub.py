import grpc
import greet_pb2
import greet_pb2_grpc

def run():
    with grpc.insecure_channel('localhost:5203') as channel:     
        stub = greet_pb2_grpc.ProductUpdaterStub(channel)    
        named_request = greet_pb2.ProductUpdateRequest()
        named_request.type.productType = greet_pb2.ProductType.GROSZEK
        named_request.quantity = 1230
        named_request.branch = "Ciechocinek"
        named_reply = stub.UpdateProduct(named_request)
        print("Response received:")
        print(named_reply)


if __name__== "__main__":
    run()