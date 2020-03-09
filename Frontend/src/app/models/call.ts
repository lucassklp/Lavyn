export class Call {
    callType: CallType;
    key: string;
}

export enum CallType {
    Audio,
    Video,
    ScreenSharing
}