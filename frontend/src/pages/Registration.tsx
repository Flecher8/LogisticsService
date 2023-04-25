import { useState, useEffect, useRef, FC } from "react";
import { Button, InputGroup, FormControl, Form } from "react-bootstrap";
import { Link } from "react-router-dom";
import { RegistrationViewModel } from "../helpers/viewModels/RegistrationViewModel";
import { registrationApi } from "../api/registrationApi";

// TODO Language
export const Registration: FC = () => {
	const inputPassword = useRef<HTMLInputElement>(null);
	const inputPasswordRepeat = useRef<HTMLInputElement>(null);
	const inputEmail = useRef<HTMLInputElement>(null);
	const inputCompanyName = useRef<HTMLInputElement>(null);
	const [inputUserType, setInputUserType] = useState<string>("PrivateCompany");

	const passworCorrect = (): boolean => {
		if (inputPassword.current?.value === inputPasswordRepeat.current?.value) {
			return true;
		}
		return false;
	};

	const isAllRequiredFieldsAreFilled = (): boolean => {
		if (
			inputCompanyName.current?.value === "" ||
			inputPassword.current?.value === "" ||
			inputPasswordRepeat.current?.value === "" ||
			inputEmail.current?.value === ""
		) {
			return false;
		}
		return true;
	};

	const register = (): void => {
		if (!passworCorrect()) {
			alert("Password is not the same");
			return;
		}
		if (!isAllRequiredFieldsAreFilled()) {
			alert("Not all required fields are filled");
			return;
		}

		const registrationViewMode: RegistrationViewModel = {
			email: inputEmail.current?.value || "",
			password: inputPassword.current?.value || "",
			companyName: inputCompanyName.current?.value || "",
			userType: inputUserType
		};

		registrationApi(registrationViewMode);
	};

	return (
		<div className="d-flex justify-content-center mt-5">
			<div className="border p-2 w-50">
				<div className="display-3 text-center mb-5">Registration</div>
				<div className="d-inline-flex w-100 p-3">
					<div className="w-25 d-inline-flex">
						<h6>Company name:</h6>
						<p className="ml-2 text-danger">*</p>
					</div>
					<div className="w-75">
						<InputGroup className="mb-3">
							<FormControl
								aria-label="Default"
								placeholder="Company"
								type="text"
								maxLength={50}
								size="sm"
								ref={inputCompanyName}
								aria-describedby="inputGroup-sizing-default"
							/>
						</InputGroup>
					</div>
				</div>
				<div className="d-inline-flex w-100 p-3">
					<div className="w-25 d-inline-flex">
						<h6>Email:</h6>
						<p className="ml-2 text-danger">*</p>
					</div>
					<div className="w-75">
						<InputGroup className="mb-3">
							<FormControl
								aria-label="Default"
								placeholder="user@gmail.com"
								type="email"
								maxLength={150}
								size="sm"
								ref={inputEmail}
								aria-describedby="inputGroup-sizing-default"
							/>
						</InputGroup>
					</div>
				</div>
				<div className="d-inline-flex w-100 p-3">
					<div className="w-25 d-inline-flex">
						<h6>Password:</h6>
						<p className="ml-2 text-danger">*</p>
					</div>
					<div className="w-75">
						<InputGroup className="mb-3">
							<FormControl
								aria-label="Default"
								placeholder="*****"
								type="password"
								maxLength={50}
								size="sm"
								ref={inputPassword}
								aria-describedby="inputGroup-sizing-default"
							/>
						</InputGroup>
					</div>
				</div>
				<div className="d-inline-flex w-100 p-3">
					<div className="w-25 d-inline-flex">
						<h6>Repeat password:</h6>
						<p className="ml-2 text-danger">*</p>
					</div>
					<div className="w-75">
						<InputGroup className="mb-3">
							<FormControl
								aria-label="Default"
								placeholder="*****"
								type="password"
								maxLength={50}
								size="sm"
								ref={inputPasswordRepeat}
								aria-describedby="inputGroup-sizing-default"
							/>
						</InputGroup>
					</div>
				</div>
				<div className="d-inline-flex w-100 p-3">
					<div className="w-25 d-inline-flex">
						<h6>Select company type:</h6>
						<p className="ml-2 text-danger">*</p>
					</div>
					{/* // TODO Do this like a TypeScript code */}
					<div className="w-75">
						<Form.Select aria-label="Default" size="sm" onChange={e => setInputUserType(e.target.value)}>
							<option value="PrivateCompany">Private company</option>
							<option value="LogisticCompany">Logistic company</option>
						</Form.Select>
					</div>
				</div>

				<div className="w-100 text-center">
					<Button onClick={register} className="w-100 text-center">
						<h5>Register</h5>
					</Button>
				</div>
			</div>
		</div>
	);
};
