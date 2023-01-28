export interface CreateSendPointCommand {
    name: string;
    code: string;
    city: string;
    street: string;
    buildingNumber: string;
    latitude: number;
    longitude: number;
    isActive: boolean;
}