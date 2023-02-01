from google.protobuf.internal import containers as _containers
from google.protobuf import descriptor as _descriptor
from google.protobuf import message as _message
from typing import ClassVar as _ClassVar, Iterable as _Iterable, Mapping as _Mapping, Optional as _Optional, Union as _Union

DESCRIPTOR: _descriptor.FileDescriptor

class Product(_message.Message):
    __slots__ = ["date", "price", "productType", "quantity", "sendPoint"]
    DATE_FIELD_NUMBER: _ClassVar[int]
    PRICE_FIELD_NUMBER: _ClassVar[int]
    PRODUCTTYPE_FIELD_NUMBER: _ClassVar[int]
    QUANTITY_FIELD_NUMBER: _ClassVar[int]
    SENDPOINT_FIELD_NUMBER: _ClassVar[int]
    date: str
    price: float
    productType: str
    quantity: int
    sendPoint: str
    def __init__(self, productType: _Optional[str] = ..., quantity: _Optional[int] = ..., sendPoint: _Optional[str] = ..., price: _Optional[float] = ..., date: _Optional[str] = ...) -> None: ...

class ProductUpdateRequest(_message.Message):
    __slots__ = ["productUpdate"]
    PRODUCTUPDATE_FIELD_NUMBER: _ClassVar[int]
    productUpdate: _containers.RepeatedCompositeFieldContainer[Product]
    def __init__(self, productUpdate: _Optional[_Iterable[_Union[Product, _Mapping]]] = ...) -> None: ...

class ProductUpdateResponse(_message.Message):
    __slots__ = ["message"]
    MESSAGE_FIELD_NUMBER: _ClassVar[int]
    message: str
    def __init__(self, message: _Optional[str] = ...) -> None: ...
