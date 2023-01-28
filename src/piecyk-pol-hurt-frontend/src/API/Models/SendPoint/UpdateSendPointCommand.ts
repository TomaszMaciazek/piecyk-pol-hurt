export interface UpdateSendPointCommand {
    id: number;
    name: string;
    code: string;
    city: string;
    street: string;
    buildingNumber: string;
    latitude: number;
    longitude: number;
    isActive: boolean;
}