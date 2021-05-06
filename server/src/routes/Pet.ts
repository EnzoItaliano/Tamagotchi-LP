import * as PetController from "../controller/PetController";
import { Router } from "express";

var router = new Router();

router.get("/", PetController.getAllPets)

router.get("/:id", PetController.getpetById)

router.post("/", PetController.savePet)

module.exports = router;