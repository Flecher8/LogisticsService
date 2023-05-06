import { useState, useEffect, useRef, useMemo, FC } from "react";
import { GoogleMap, LoadScript, Marker, useLoadScript, MarkerF } from "@react-google-maps/api";

interface GoogleMapsProps {
	lat: number;
	lng: number;
}

export const GoogleMaps: FC<GoogleMapsProps> = ({ lat, lng }) => {
	const { isLoaded } = useLoadScript({
		googleMapsApiKey: process.env.REACT_APP_GOOGLE_MAPS_API_KEY || ""
	});

	const containerStyle = {
		height: "300px"
	};

	const handleClick = (event: any) => {
		const lat = event.latLng.lat();
		const lng = event.latLng.lng();

		console.log("Lat:", lat);
		console.log("Lng:", lng);
	};

	const center = useMemo(() => ({ lat: lat, lng: lng }), []);

	useEffect(() => {}, []);
	const divStyle = {
		background: `white`,
		border: `1px solid #ccc`,
		padding: 15
	};

	return (
		<div className="GoogleMaps">
			{isLoaded && (
				<GoogleMap mapContainerStyle={containerStyle} center={center} zoom={10}>
					<MarkerF position={center} />
				</GoogleMap>
			)}
		</div>
	);
};
