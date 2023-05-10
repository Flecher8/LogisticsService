import { useRef, FC } from "react";
import { Button, InputGroup, FormControl } from "react-bootstrap";
import { Link } from "react-router-dom";
import LoginService from "../api/services/LoginService";
import { useTranslationHelper } from "../helpers/translation/translationService";

const loginService = new LoginService();

// TODO Language
export const Login: FC = () => {
	const { t, changeLanguage } = useTranslationHelper();

	const inputEmail = useRef<HTMLInputElement>(null);
	const inputPassword = useRef<HTMLInputElement>(null);

	const login = async (): Promise<void> => {
		try {
			await loginService.login(inputEmail.current?.value || "", inputPassword.current?.value || "");
		} catch (err: any) {
			alert(t(err));
		}
	};

	return (
		<div className="d-flex justify-content-center mt-5">
			<div className="border p-2">
				<div className="display-3 text-center mb-5">Sign in</div>
				<div className="d-inline-flex w-100 p-3">
					<div className="w-25 d-inline-flex">
						<h4>{t("Login")}:</h4> <p className="ml-2 text-danger">*</p>
					</div>
					<div className="w-75">
						<InputGroup className="mb-3">
							<FormControl
								aria-label="Default"
								placeholder="login"
								type="text"
								size="sm"
								maxLength={50}
								ref={inputEmail}
								aria-describedby="inputGroup-sizing-default"
							/>
						</InputGroup>
					</div>
				</div>
				<div className="d-inline-flex w-100 p-3">
					<div className="w-25 d-inline-flex">
						<h4>{t("Password")}:</h4> <p className="ml-2 text-danger">*</p>
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
				<div className="w-100 text-center">
					<Button onClick={login} className="w-100 text-center">
						<h5>{t("Sign in")}</h5>
					</Button>
				</div>
				<div className="d-inline-flex justify-content-center w-100 m-2">
					<div className="p-2">{t("Not registered yet")}?</div>
					<div className="p-2">
						<Link to="/Registration" className="text-decoration-none">
							{t("Registration")}
						</Link>
					</div>
				</div>
			</div>
		</div>
	);
};
