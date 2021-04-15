import {Entity, Column,  PrimaryGeneratedColumn, ManyToOne, JoinColumn} from "typeorm";
import { Account } from "./Account";

@Entity("order")
export class Pet {

    @PrimaryGeneratedColumn()
    id: number;

    @Column()
    name: string;

    @ManyToOne(type => Account, account => account.pets, {onDelete:'CASCADE'})
    @JoinColumn({ name: "AccountId" })
    account: Account;
}