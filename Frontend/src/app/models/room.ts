import { Message } from './message';
import { User } from './user';
import { ViewedMessage } from './viewed-message';

export class Room {
    key: string;
    name: string;
    participants: User[];
    messages: Message[];
    lastViews: ViewedMessage[];
}