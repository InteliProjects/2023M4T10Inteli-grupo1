import React from 'react';
import styled from 'styled-components';

const Card = styled.div`
  background-color: #f5f5f5;
  border-radius: 8px;
  padding: 16px;
  margin: 8px;
  display: flex;
  flex-direction: column;
`;

const Title = styled.h2`
  font-size: 18px;
  font-weight: bold;
  margin-bottom: 8px; 
`;

const StatusBar = styled.div`
  display: flex;
  align-items: center;
  margin-top: 8px;
  position: relative;
`;

const StatusDot = styled.div`
  width: 12px;
  height: 12px;
  border-radius: 50%;
  margin-right: 4px; /* Reduz a margem entre a bolinha e o texto */
  background-color: ${(props) => (props.active ? 'green' : 'gray')};
`;

const StatusText = styled.span`
  font-size: 12px;
  color: ${(props) => (props.active ? 'green' : 'gray')};
  font-weight: bold;
`;

const Line = styled.div`
  flex: 1;
  height: 2px;
  width: 100%;
  margin-bottom: 15px;
  background-color: ${(props) => (props.active ? 'green' : 'gray')};
`;

const OrderCard = ({ nome, items, status }) => {
  return (
    <Card>
      <Title>OS: {nome}</Title>
      <p>{items}</p>
      <StatusBar>
        {[...Array(5)].map((_, index) => (
          <React.Fragment key={index}>
            <div style={{ flex: 1, display: 'flex', flexDirection: 'column', alignItems: 'center' }}>
              <StatusDot active={index <= status} />
              <StatusText active={index === status}>
                {index === 0 && 'Não Iniciada'}
                {index === 1 && 'Iniciada'}
                {index === 2 && 'Atrasada'}
                {index === 3 && 'Finalizada'}
                {index === 4 && 'Impedida'}
              </StatusText>
            </div>
            {index < 4 && <Line active={index < status} />}
          </React.Fragment>
        ))}
      </StatusBar>
    </Card>
  );
};

export default OrderCard;