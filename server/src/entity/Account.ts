import { Entity, Column, PrimaryGeneratedColumn, OneToMany, JoinColumn } from "typeorm";
import { Pet } from "./Pet";

@Entity("account")
export class Account {

    @PrimaryGeneratedColumn()
    id: number;

    @Column()
    username: string;

    @Column()
    password: string;

    @OneToMany(type => Pet, pet => pet.account)
    pets: Pet[];
    
}