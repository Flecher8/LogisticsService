import { useState, useEffect, FC } from "react";
import { Button, Card } from "react-bootstrap";
import { Link } from "react-router-dom";
import { AdminPanel } from "../../components/AdminPanel";
import { PrivateCompanyPanel } from "../../components/PrivateCompanyPanel";
import { PrivateCompaniesService, PrivateCompany } from "../../api/services/PrivateCompaniesService";

export const PrivateCompanyProfile: FC = () => {
	const [profile, setProfile] = useState<PrivateCompany | null>(null);

	const getProfileData = async (): Promise<void> => {
		try {
			const userId: number = parseInt(localStorage["userId"]);
			const response: PrivateCompany | null = await PrivateCompaniesService.prototype.getItem(userId);
			setProfile(response);
		} catch (err) {
			alert(err);
		}
	};

	useEffect(() => {
		getProfileData();
	}, []);
	return (
		<div className="PrivateCompanyProfile container">
			{/* // TODO Language */}
			<div className="d-flex border border-dark w-100">
				<PrivateCompanyPanel />
			</div>
			<div>
				<header>
					<div className="text-center mt-5">
						<h1>Profile</h1>
					</div>
				</header>
				{profile && (
					<Card className="my-5">
						<Card.Header>Profile Information</Card.Header>
						<Card.Body>
							<Card.Text>{`Company name: ${profile.companyName}`}</Card.Text>
							<Card.Text>{`Email: ${profile.email}`}</Card.Text>
							<Card.Text>{`Description: ${profile.description === null ? "" : profile.description}`}</Card.Text>
						</Card.Body>
					</Card>
				)}
			</div>
		</div>
	);
};
