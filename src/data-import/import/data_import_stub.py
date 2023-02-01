import grpc
import greet_pb2_grpc

def send(message):
    with grpc.insecure_channel('localhost:5203') as channel:     
        stub = greet_pb2_grpc.ProductUpdaterStub(channel)
        named_reply = stub.UpdateProduct(message)
        print("Response received:")
        print(named_reply)


if __name__== "__main__":
    send()