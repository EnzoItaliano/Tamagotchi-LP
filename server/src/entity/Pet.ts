import {Entity, Column,  PrimaryGeneratedColumn, ManyToOne, JoinColumn} from "typeorm";
import { Account } from "./Account";

@Entity("order")
export class Pet {

    @PrimaryGeneratedColumn()
    id: number;

    @Column()
    name: string;

    @Column()
    outfit: number;

    @Column()
    happy: number;

    @Column()
    hunger: number;

    @Column()
    health: number;

    @Column()
    tired: number;

    @Column()
    dirty: number;

    @Column()
    sad: number;

    @Column()
    sleeping: number;

    @Column()
    normal: boolean;

    @Column()
    sick: boolean;

    @Column()
    dead: boolean;

    @Column()
    light: boolean;

    @Column()
    time: string;

    @ManyToOne(type => Account, account => account.pets, {onDelete:'CASCADE'})
    @JoinColumn({ name: "AccountId" })
    account: Account;
}
