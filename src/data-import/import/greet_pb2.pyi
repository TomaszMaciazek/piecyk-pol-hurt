from google.protobuf.internal import enum_type_wrapper as _enum_type_wrapper
from google.protobuf import descriptor as _descriptor
from google.protobuf import message as _message
from typing import ClassVar as _ClassVar, Mapping as _Mapping, Optional as _Optional, Union as _Union

DESCRIPTOR: _descriptor.FileDescriptor

class ProductType(_message.Message):
    __slots__ = ["productType"]
    class PRODUCT_TYPE(int, metaclass=_enum_type_wrapper.EnumTypeWrapper):
        __slots__ = []
    GROSZEK: ProductType.PRODUCT_TYPE
    KOSTKA: ProductType.PRODUCT_TYPE
    MIAL: ProductType.PRODUCT_TYPE
    ORZECH: ProductType.PRODUCT_TYPE
    PRODUCTTYPE_FIELD_NUMBER: _ClassVar[int]
    productType: ProductType.PRODUCT_TYPE
    def __init__(self, productType: _Optional[_Union[ProductType.PRODUCT_TYPE, str]] = ...) -> None: ...

class ProductUpdateRequest(_message.Message):
    __slots__ = ["branch", "quantity", "type"]
    BRANCH_FIELD_NUMBER: _ClassVar[int]
    QUANTITY_FIELD_NUMBER: _ClassVar[int]
    TYPE_FIELD_NUMBER: _ClassVar[int]
    branch: str
    quantity: int
    type: ProductType
    def __init__(self, type: _Optional[_Union[ProductType, _Mapping]] = ..., quantity: _Optional[int] = ..., branch: _Optional[str] = ...) -> None: ...

class ProductUpdateResponse(_message.Message):
    __slots__ = ["message"]
    MESSAGE_FIELD_NUMBER: _ClassVar[int]
    message: str
    def __init__(self, message: _Optional[str] = ...) -> None: ...
