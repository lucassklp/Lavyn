export class Call {
    callerId?: number;
    callType?: CallType;
    key?: string;
}

export enum CallType {
    Audio,
    Video,
    ScreenSharing
}