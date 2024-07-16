/** @jsxImportSource @emotion/react */
import { css } from '@emotion/react';
import * as React from "react";
import { TranslationJob } from "./TranslationJob";

// Styles moved outside the component for better readability
const listStyle = css`
  list-style: none;
  margin: 10px 0 0 0;
  padding: 0 20px;
  background-color: #fff;
  border-bottom-left-radius: 4px;
  border-bottom-right-radius: 4px;
`;

interface Props {
  data: TranslationJob[];
  renderItem?: (item: TranslationJob) => JSX.Element;
}

export const JobList: React.FC<Props> = ({ data, renderItem }) => {
  // Function to render each item, uses renderItem prop if available
  const renderListItem = (item: TranslationJob, index: number): JSX.Element => {
    if (renderItem) {
      return renderItem(item);
    }
    // Default rendering if no renderItem provided
    return <li key={index}>{item.id}</li>;
  };

  // Only render the list if data is not empty
  if (data.length === 0) {
    return null;
  }

  return (
    <div>
      <h2>JobList</h2>
      <ul css={listStyle}>
        {data.map((item, index) => renderListItem(item, index))}
      </ul>
    </div>
  );
};