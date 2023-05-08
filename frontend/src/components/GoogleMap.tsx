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

	const center = useMemo(() => ({ lat: lat, lng: lng }), []);

	useEffect(() => {}, []);

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
