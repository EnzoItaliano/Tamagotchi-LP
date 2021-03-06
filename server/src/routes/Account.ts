import * as UserController from "../controller/UserController";
import { Router } from "express";

var router = new Router();

router.get("/", UserController.getAllUsers)

router.post("/", UserController.saveUser)

router.get("/:id", UserController.getuserById)

router.delete("/:id", UserController.deleteUser)

module.exports = router;