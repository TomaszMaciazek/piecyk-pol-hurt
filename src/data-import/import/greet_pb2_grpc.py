# Generated by the gRPC Python protocol compiler plugin. DO NOT EDIT!
"""Client and server classes corresponding to protobuf-defined services."""
import grpc

import greet_pb2 as greet__pb2


class ProductUpdaterStub(object):
    """The greeting service definition.
    """

    def __init__(self, channel):
        """Constructor.

        Args:
            channel: A grpc.Channel.
        """
        self.UpdateProduct = channel.unary_unary(
                '/productUpdate.ProductUpdater/UpdateProduct',
                request_serializer=greet__pb2.ProductUpdateRequest.SerializeToString,
                response_deserializer=greet__pb2.ProductUpdateResponse.FromString,
                )


class ProductUpdaterServicer(object):
    """The greeting service definition.
    """

    def UpdateProduct(self, request, context):
        """Sends a greeting
        """
        context.set_code(grpc.StatusCode.UNIMPLEMENTED)
        context.set_details('Method not implemented!')
        raise NotImplementedError('Method not implemented!')


def add_ProductUpdaterServicer_to_server(servicer, server):
    rpc_method_handlers = {
            'UpdateProduct': grpc.unary_unary_rpc_method_handler(
                    servicer.UpdateProduct,
                    request_deserializer=greet__pb2.ProductUpdateRequest.FromString,
                    response_serializer=greet__pb2.ProductUpdateResponse.SerializeToString,
            ),
    }
    generic_handler = grpc.method_handlers_generic_handler(
            'productUpdate.ProductUpdater', rpc_method_handlers)
    server.add_generic_rpc_handlers((generic_handler,))


 # This class is part of an EXPERIMENTAL API.
class ProductUpdater(object):
    """The greeting service definition.
    """

    @staticmethod
    def UpdateProduct(request,
            target,
            options=(),
            channel_credentials=None,
            call_credentials=None,
            insecure=False,
            compression=None,
            wait_for_ready=None,
            timeout=None,
            metadata=None):
        return grpc.experimental.unary_unary(request, target, '/productUpdate.ProductUpdater/UpdateProduct',
            greet__pb2.ProductUpdateRequest.SerializeToString,
            greet__pb2.ProductUpdateResponse.FromString,
            options, channel_credentials,
            insecure, call_credentials, compression, wait_for_ready, timeout, metadata)
