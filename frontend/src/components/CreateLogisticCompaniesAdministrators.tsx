import { useState, useEffect, FC } from "react";
import { Link } from "react-router-dom";
import { Button, InputGroup, FormControl, Table, Modal, Form } from "react-bootstrap";
import { CreateModelProps } from "../helpers/interfaces/ChangeModelProps";
import {
	LogisticCompaniesAdministratorDto,
	LogisticCompaniesAdministratorsService
} from "../api/services/LogisticCompaniesAdministratorsService";

export const CreateLogisticCompaniesAdministrators: FC<CreateModelProps> = (props: CreateModelProps) => {
	const [firstName, setFirstName] = useState<string>("");
	const [lastName, setLastName] = useState<string>("");
	const [email, setEmail] = useState<string>("");
	const [password, setPassword] = useState<string>("");

	async function handle() {
		try {
			let administrator: LogisticCompaniesAdministratorDto = {
				firstName: firstName,
				lastName: lastName,
				email: email,
				password: password,
				logisticCompanyId: parseInt(localStorage["userId"])
			};
			await LogisticCompaniesAdministratorsService.prototype.create(administrator);
			props.close();
			window.location.reload();
		} catch (err) {
			alert(err);
		}
	}

	return (
		<div className="CreateLogisticCompaniesAdministrators">
			<div className="container">
				<header>
					<div className="text-center mt-5">
						<h1>Create</h1>
					</div>
				</header>
				<div>
					<div className="mt-5 ml-5 mr-5">
						<Form>
							<Form.Group className="mb-3">
								<Form.Label>First name:</Form.Label>
								<Form.Control
									type="text"
									placeholder="Tom"
									value={firstName}
									onChange={e => setFirstName(e.target.value)}
								/>
							</Form.Group>

							<Form.Group className="mb-3">
								<Form.Label>Last name:</Form.Label>
								<Form.Control
									type="text"
									placeholder="Care"
									value={lastName}
									onChange={e => setLastName(e.target.value)}
								/>
							</Form.Group>

							<Form.Group className="mb-3">
								<Form.Label>Email:</Form.Label>
								<InputGroup>
									<FormControl
										type="text"
										placeholder="email@gmail.com"
										value={email}
										onChange={e => setEmail(e.target.value)}
									/>
								</InputGroup>
							</Form.Group>
							<Form.Group className="mb-3">
								<Form.Label>Password:</Form.Label>
								<InputGroup>
									<FormControl
										type="password"
										placeholder="******"
										value={password}
										onChange={e => setPassword(e.target.value)}
									/>
								</InputGroup>
							</Form.Group>
						</Form>
						<div>
							<Button className="mb-5" onClick={() => handle()}>
								Create
							</Button>
						</div>
					</div>
				</div>
			</div>
		</div>
	);
};
