import { FC } from "react";
import { Container, Row, Col, Image, Card } from "react-bootstrap";
import { useTranslationHelper } from "../helpers/translation/translationService";

export const Home: FC = () => {
	const { t, changeLanguage } = useTranslationHelper();
	return (
		<div className="Home">
			<Container>
				<Card>
					<h1>{t("Software system for monitoring the delivery of goods by logistics organizations")}</h1>
					<p>
						{t(
							"This project was created to solve the problem of efficient delivery of goods to private companies through logistics organizations."
						)}
						.
					</p>
				</Card>
				<Row>
					<Col md={6}>
						<Image src="https://imperadorlogistics.com/wp-content/uploads/2017/10/banner3.jpg" fluid />
					</Col>
					<Col md={6}>
						<p>
							{t(
								"Currently, many private companies are faced with the problem of choosing the best logistics company that can deliver their goods on time and with high quality. On the other hand, logistics companies often face the challenge of managing their resources, including vehicles, workers, and technology."
							)}
						</p>
						<p>
							{t(
								"The purpose of this project is to create a software system that will help private companies to efficiently select logistics companies and control the delivery of their goods, as well as help logistics companies manage their resources and ensure high quality delivery. This system will be used to streamline goods delivery processes and improve customer satisfaction by providing more accurate real-time tracking of shipments and improved communication between private companies and logistics organizations."
							)}
						</p>
					</Col>
				</Row>
				<Row className="mt-5">
					<Col md={6}>
						<p>{t("The system will implement:")}</p>
						<ul>
							<li>{t("Choosing a logistics company based on price, quality and delivery time")}</li>
							<li>{t("Real-time cargo tracking")}</li>
							<li>{t("Logistics company resource management")}</li>
							<li>{t("Goods delivery reporting")}</li>
						</ul>
					</Col>
					<Col md={6}>
						<Image src="https://3pllinks.com/wp-content/uploads/2022/10/largest-logistics-companies.jpg" fluid />
					</Col>
				</Row>
			</Container>
		</div>
	);
};
